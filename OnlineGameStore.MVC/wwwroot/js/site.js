// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
const toKebabCase = str =>
  str &&
  str
    .match(/[A-Z]{2,}(?=[A-Z][a-z]+\d*|\b)|[A-Z]?[a-z]+\d*|[A-Z]|\d+/g)
    .map(x => x.toLowerCase())
        .join('-');

let transformToKebabCase = function (source, targetName) {
    let form = source.closest('form');
    let target = form[targetName];
    target.value = toKebabCase(source.value);
};

$(function () {
    $.ajaxSetup({cache: false});
    
    $(".modal-link").click(function (event) {
        event.preventDefault();
        $.get(this.href, function (data) {
            $('#dialogContent').html(data);
            $('#modalDialog').modal('show');
        });
    });

    $(".btn-reply").click(function (event) {
        let button = event.target;
        const quoteValue = 'Quote';
        let commentKindValue = event.target.dataset.commentKind;
        let card = button.closest('.card-body');
        let form = document.forms['addCommentForm'];
        let replyToIdInput = form['ReplyToId'];
        let isQuotedInput = form['IsQuoted'];
        let id = card.id.replace(/^\D+/g, '');
        if (replyToIdInput.value === '') {
            card.classList.add('bg-warning');
            button.textContent = 'Not ' + commentKindValue;
            replyToIdInput.value = id;
            isQuotedInput.value = quoteValue === commentKindValue;
        } else if (replyToIdInput.value !== '' && replyToIdInput.value === id) {
            card.classList.remove('bg-warning');
            button.textContent = commentKindValue;
            replyToIdInput.value = '';
            isQuotedInput.value = false;
        } else {
            let elems = document.querySelectorAll(".card-body");
            [].forEach.call(elems, function (el) {
                el.classList.remove("bg-warning");
            });
            card.classList.add('bg-warning');
            elems = document.querySelectorAll(".btn-reply");
            [].forEach.call(elems, function (el) {
                el.textContent = commentKindValue;
            });
            button.textContent = 'Not ' + commentKindValue;
            replyToIdInput.value = id;
            isQuotedInput.value = quoteValue === commentKindValue;
        }
    });
});

function OnSuccess(result) {
    if (result.url) {
        window.location.href = result.url;
    }
}
