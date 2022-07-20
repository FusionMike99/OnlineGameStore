$(function () {
    const initialMin = 0.01;
    const initialMax = 99999999.99;
    
    $("#PriceRange_Min").change(function (){
        let val = $(this).val();
        let selector = "#PriceRange_Max";
        $(selector).rules( "remove", "min" );
        $(selector).rules( "add", {
            min: val ? val : initialMin
        });
    });

    $("#PriceRange_Max").change(function (){
        let val = $(this).val();
        let selector = "#PriceRange_Min";
        $(selector).rules( "remove", "max" );
        $(selector).rules( "add", {
            max: val ? val : initialMax
        });
    });
    
    $("#sort-filter-form").on("reset", function (event){
        event.preventDefault();
        $("input:checkbox").prop("checked", false);
        $("input:radio:first").attr("checked", true);
        $("#GameSortState").val($("#GameSortState option:first").val());
        $("input:text").val("");
    });

    jQuery.validator.addMethod("noSpace", function(value, element) {
        return !(/^\s*$/.test(value) && value.length);
    }, "The field must not contain white spaces.");

    $("#GameName").rules( "add", {
        noSpace: true
    });
});