using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrestamosMVC5.Models
{
    public class HtmlClass
    {
        /// <summary>
        /// html class for div tags that wrap a group of control
        /// </summary>
        public static string FormGroupCls => "form-group row";
        public static string LabelCls => "control-label col-md-3 col-sm-3 col-xs-3";
        /// <summary>
        /// html class for div tags that wrap an input control
        /// </summary>
        public static string DivInputCls => "col-md-9 col-sm-9 col-xs-9";
        /// <summary>
        /// html class for input tag
        /// </summary>
        public static string InputTextCls => "form-control";
        public static string CheckBoxCls => "js-switch";
        public static string TextAreaResizableCls => "resizable_textarea form-control";
        public static string SpanfauserCls => "fa fa-user form-control-feedback right";
        /// <summary>
        /// span for phone info
        /// </summary>
        public static string SpanfaphoneCls => "fa fa-phone form-control-feedback right";
        /// <summary>
        /// to use at street info
        /// </summary>
        public static string SpanAddressDetailCls => "fa fa-home form-control-feedback right";
        /// <summary>
        /// to use at gps and other similar cases
        /// </summary>
        public static string SpanLocationCls => "fa fa-location-arrow form-control-feedback right";
        public static string SpanZipCodeCls  => "fa fa-map-pin form-control-feedback right";
        public static string SpanDetailsCls  => "fa fa-info-circle form-control-feedback right";
        public static string SpanWorkInfoCls => "fa fa-wrench form-control-feedback right";
    }
}