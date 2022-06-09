$(function () {
    function getQuery(){
        let query = window.location.search;
        query = query.slice(1);
        query = query.replace(/&{0,2}PageNumber=-?\w*&{0,2}/g,'');
        return query;
    }
    
    $(".page-link").click(function (event) {
        let anchor = event.currentTarget;
        let query = getQuery();
        if(query){
            query = "&" + query;
        }
        anchor.href += query;
    });
    
    $("#page-size-selector").change(function (event){
        let selector = event.currentTarget;
        let query = getQuery();
        query = query.replace(/&{0,2}PageSize=\d*&{0,2}/g, '');
        if(query){
            query = "&" + query;
        }
        query ='?PageSize=' + selector.value + query;
        window.location.search = query;
    });

    $('#PageNumber').change(function() {
        let min = parseInt($(this).attr('min'));
        let max = parseInt($(this).attr('max'));
        let val = $(this).val();
        if(val) {
            if (val < min) {
                $(this).val(min);
            } else if (val > max) {
                $(this).val(max);
            }
        }
    });

    let tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    let tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl)
    });
})