class CurrencyNumberTS {
    formatNumber(n: string) {
        // format number 1234567 to 1,234,567
        return n.replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, ",")
    }

    formatCurrency(input: JQuery, blur: string) {

        // appends $ to value, validates decimal side
        // and puts cursor back in right position.

        // get input value
        
        
        let inputVal = input.val();
        // don't validate empty input
        if (inputVal === "") { return; }

        if (inputVal.substr(1, 4) === "0.00" && inputVal.length > 5) {
            inputVal = inputVal.replace("0.00", '');
        }
        
        // original length
        const originalLen = inputVal.length;

        // initial caret position
        let caretPos = input.prop("selectionStart");

        // check for decimal
        if (inputVal.indexOf(".") >= 0) {

            // get position of first decimal
            // this prevents multiple decimals from
            // being entered
            const decimalPos = inputVal.indexOf(".");


            // split number by decimal point
            let leftSide = inputVal.substring(0, decimalPos);
            let rightSide = inputVal.substring(decimalPos);
            // add commas to left side of number
            leftSide = this.formatNumber(leftSide);

            // validate right side

            rightSide = this.formatNumber(rightSide);

            // On blur make sure 2 numbers after decimal
            if (blur === "blur") {
                rightSide += "00";
            }

            // Limit decimal to only 2 digits
            rightSide = rightSide.substring(0, 2);
            // join number by .
            inputVal = "$" + leftSide + "." + rightSide;

        } else {
            // no decimal entered
            // add commas to number
            // remove all non-digits
            inputVal = this.formatNumber(inputVal);
            inputVal = "$" + inputVal;

            // final formatting
            if (blur === "blur") {
                inputVal += ".00";
            }
        }

        // send updated string to input
        input.val(inputVal);
        //console.log(input.val());
        //input.attr("value", inputVal);
        //console.log("valor del input ", input.val(), "nombre ", input.attr("name"), input.attr("value"));
        // put caret back in the right position
        const updatedLen = inputVal.length;
        caretPos = updatedLen - originalLen + caretPos;
        (input[0] as HTMLInputElement).setSelectionRange(caretPos, caretPos);
    }

    toFloat(num: string): number {
        if (num.length === 0) return 0;
        let dotPos = num.indexOf('.');
        let commaPos = num.indexOf(',');
        let sep = 0;

        if (dotPos < 0) {
            dotPos = 0;
        }

        if (commaPos < 0)
            commaPos = 0;

        if ((dotPos > commaPos) && dotPos)
            sep = dotPos;
        else {
            if ((commaPos > dotPos) && commaPos)
                sep = commaPos;
        }
        const result = parseFloat(
            num.substr(0, sep).replace(/[^\d]/g, "") + '.' +
            num.substr(sep + 1, num.length).replace(/[^0-9]/, "")

        );
        return result;
    }
    //Usage: toFloat("$1,100.00") or toFloat("1,100.00$")
}

function getElements(instance) {
    // formato de signo de peso alante con comas y los decimales luego del punto
    $("input[data-type='currency']").on({
        keyup: function () {
             instance.formatCurrency($(this));
        },
        blur: function () {
            instance.formatCurrency ($(this), "blur");
        }
    });
}


