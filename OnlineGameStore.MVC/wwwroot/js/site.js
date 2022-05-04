// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

const toKebabCase = str =>
  str &&
  str
    .match(/[A-Z]{2,}(?=[A-Z][a-z]+[0-9]*|\b)|[A-Z]?[a-z]+[0-9]*|[A-Z]|[0-9]+/g)
    .map(x => x.toLowerCase())
        .join('-');

let transformToKebabCase = function (source, targetName) {
    let form = source.closest('form');

    let target = form[targetName];

    target.value = toKebabCase(source.value);
};

let setReplyToId = function (event) {
    let button = event.target.closest('.btn-reply');

    if (!button) return;

    let card = button.closest('.card-body');

    let form = document.forms['addCommentForm'];

    let replyToIdInput = form['ReplyToId'];

    let id = card.id.replace(/^\D+/g, '');

    if (replyToIdInput.value == '') {
        card.classList.add('bg-warning');
        button.textContent = 'Unreply';

        replyToIdInput.value = id;
    }
    else if (replyToIdInput.value != '' && replyToIdInput.value == id) {
        card.classList.remove('bg-warning');
        button.textContent = 'Reply';

        replyToIdInput.value = '';
    }
    else {
        var elems = document.querySelectorAll(".card-body");

        [].forEach.call(elems, function (el) {
            el.classList.remove("bg-warning");
        });

        card.classList.add('bg-warning');

        elems = document.querySelectorAll(".btn-reply");

        [].forEach.call(elems, function (el) {
            el.textContent = 'Reply';
        });

        button.textContent = 'Unreply';

        replyToIdInput.value = id;
    }
};