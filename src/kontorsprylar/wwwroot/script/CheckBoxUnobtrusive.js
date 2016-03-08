var $jQval = $.validator,
    adapters,
    data_validation = "unobtrusiveValidation";

function setValidationValues(options, ruleName, value) {
    options.rules[ruleName] = value;
    if (options.message) {
        options.messages[ruleName] = options.message;
    }
}

$jQval.addMethod("mustbetrue", function (value, element, param) {
    // check if dependency is met
    if (!this.depend(param, element))
        return "dependency-mismatch";
    return element.checked;
});

$.validator.unobtrusive.adapters.add("mustbetrue", function (options) {
    setValidationValues(options, "mustbetrue", true);
});

$jQval.unobtrusive.parse();