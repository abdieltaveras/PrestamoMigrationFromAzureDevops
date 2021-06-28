using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace PcpUtilidades
{
    public enum ValidationsResult { Success = 1, Fail }

    public class Validator<T> : IEnumerable<ValidationRule<T>>
    {
        private readonly List<ValidationRule<T>> _rules;

        public Validator(IEnumerable<ValidationRule<T>> rules)
        {
            if (rules == null) throw new ArgumentNullException(nameof(rules));

            _rules = rules.ToList();
        }

        public static Validator<T> Empty => new Validator<T>(Enumerable.Empty<ValidationRule<T>>());

        public Validator<T> Add(ValidationRule<T> rule)
        {
            if (rule == null) throw new ArgumentNullException(nameof(rule));

            return new Validator<T>(_rules.Concat(new[] { rule }));
        }

        public IEnumerable<IValidation<T>> Validate(T obj)
        {
            foreach (var rule in _rules)
            {
                if (rule.IsMet(obj))
                {
                    yield return PassedValidation<T>.Create(rule, rule.Message);
                }
                else
                {
                    yield return FailedValidation<T>.Create(rule, rule.Message);
                    if (rule.Options.HasFlag(ValidationOptions.StopOnFailure))
                    {
                        yield break;
                    }
                }
            }
        }

        public IEnumerator<ValidationRule<T>> GetEnumerator()
        {
            return _rules.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static Validator<T> operator +(Validator<T> validator, ValidationRule<T> rule)
        {
            return validator.Add(rule);
        }
    }


    public interface IValidation<T>
    {
        bool Success { get; }

        string Expression { get; }

        string Message { get; }
    }

    public class ValidationRule<T>
    {
        private readonly Lazy<string> _expressionString;

        private readonly Lazy<Func<T, bool>> _predicate;

        public readonly string Message;

        public ValidationRule(Expression<Func<T, bool>> expression, string message, ValidationOptions options)
        {
            if (expression == null) throw new ArgumentNullException(nameof(expression));

            _predicate = new Lazy<Func<T, bool>>(() => expression.Compile());
            _expressionString = new Lazy<string>(() => CreateExpressionString(expression));
            Message = message;
            Options = options;
        }

        public ValidationOptions Options { get; }

        private static string CreateExpressionString(Expression<Func<T, bool>> expression)
        {
            var typeParameterReplacement = Expression.Parameter(typeof(T), $"<{typeof(T).Name}>");
            return ReplaceVisitor.Replace(expression.Body, expression.Parameters[0], typeParameterReplacement).ToString();
        }

        public bool IsMet(T obj) => _predicate.Value(obj);

        public override string ToString() => _expressionString.Value;

        public static implicit operator string(ValidationRule<T> rule) => rule?.ToString();
    }
    public abstract class Validation<T> : IValidation<T>
    {
        protected Validation(bool success, string expression, string message)
        {
            Success = success;
            Expression = expression;
            Message = message;
        }

        public bool Success { get; }

        public string Expression { get; }

        public string Message { get; }


    }

    internal class PassedValidation<T> : Validation<T>
    {
        private PassedValidation(string rule, string message) : base(true, rule, message) { }

        public static IValidation<T> Create(string rule, string message) => new PassedValidation<T>(rule, message);

        public override string ToString() => $"{Expression}: Passed {Message}";
    }

    internal class FailedValidation<T> : Validation<T>
    {

        private FailedValidation(string rule, string message) : base(false, rule, message) { }


        public static IValidation<T> Create(string rule, string message) => new FailedValidation<T>(rule, message);

        public override string ToString() => $"{Expression}: Failed {Message}";
    }

    public class ReplaceVisitor : ExpressionVisitor
    {
        private readonly ParameterExpression _fromParameter;
        private readonly ParameterExpression _toParameter;

        private ReplaceVisitor(ParameterExpression fromParameter, ParameterExpression toParameter)
        {
            _fromParameter = fromParameter;
            _toParameter = toParameter;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return node.Equals(_fromParameter) ? _toParameter : base.VisitParameter(node);
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            // Extract member name from closures.
            if (node.Expression is ConstantExpression)
            {
                return Expression.Parameter(node.Type, node.Member.Name);
            }

            return base.VisitMember(node);
        }

        protected override Expression VisitUnary(UnaryExpression node)
        {
            // Remove type conversion, this is change (Convert(<T>) != null) to (<T> != null)
            if (node.Operand.Type == _fromParameter.Type)
            {
                return Expression.Parameter(node.Operand.Type, _toParameter.Name);
            }

            return base.VisitUnary(node);
        }

        public static Expression Replace(Expression target, ParameterExpression from, ParameterExpression to)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));
            if (from == null) throw new ArgumentNullException(nameof(from));
            if (to == null) throw new ArgumentNullException(nameof(to));

            return new ReplaceVisitor(from, to).Visit(target);
        }
    }



    public static class ValidatorComposer
    {
        public static Validator<T> IsValidWhen<T>(this Validator<T> validator, Expression<Func<T, bool>> expression, string message, ValidationOptions options = ValidationOptions.None)
        {
            return validator + new ValidationRule<T>(expression, message, options);
        }

        public static Validator<T> IsNotValidWhen<T>(this Validator<T> validator, Expression<Func<T, bool>> expression, string message, ValidationOptions options = ValidationOptions.None)
        {
            var notExpression = Expression.Lambda<Func<T, bool>>(Expression.Not(expression.Body), expression.Parameters[0]);
            return validator.IsValidWhen(notExpression, message, options);
        }
    }

    public static class ValidatorExtensions
    {
        public static IEnumerable<IValidation<T>> ValidateWith<T>(this T obj, Validator<T> validator)
        {
            return validator.Validate(obj);
        }

        public static bool AllSuccess<T>(this IEnumerable<IValidation<T>> validations)
        {
            if (validations == null) throw new ArgumentNullException(nameof(validations));

            return validations.All(v => v.Success);
        }

        public static void ThrowIfInvalid<T>(this IEnumerable<IValidation<T>> validations)
        {
            if (validations.AllSuccess())
            {
                return;
            }

            var requirements = validations.Aggregate(
                new StringBuilder(),
                (result, validation) => result.AppendLine($"{validation.Expression} == {validation.Success}")
            ).ToString();

            throw new ValidationObjectException
            (
                name: $"{typeof(T).Name} validacion {nameof(Exception)}",
                message: $"El objeto '{typeof(T).Name}' no reune los requisitos de validacion de uno o mas condiciones" +
                $"{Environment.NewLine}{Environment.NewLine}{requirements}",
                innerException: null
            );
        }


        /// <summary>
        /// Dumps the object as a json string
        /// Can be used for logging object contents.
        /// </summary>
        /// <typeparam name="T">Type of the object.</typeparam>
        /// <param name="obj">The object to dump. Can be null</param>
        /// <param name="indent">To indent the result or not</param>
        /// <returns>the a string representing the object content</returns>
        public static string Dump<T>(this T obj, bool indent = false)
        {
            return JsonConvert.SerializeObject(obj, indent ? Formatting.Indented : Formatting.None,
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

    }

    public class ValidationObjectException : Exception
    {
        string name { get; set; }

        public ValidationObjectException(string name, string message, Exception innerException) : base(message, innerException)
        {
            this.name = name;
        }

    }


    [Flags]
    public enum ValidationOptions
    {
        None = 0,
        StopOnFailure = 1 << 0,
    }
    

}
