class Quote extends HTMLElement {
    constructor() {
        super();

        const shadow = this.attachShadow({mode: 'open'});
        const linkElem = document.createElement('link');
        linkElem.setAttribute('rel', 'stylesheet');
        linkElem.setAttribute('href', '/lib/bootstrap/css/bootstrap.min.css');
        const figure = document.createElement('figure');
        figure.setAttribute('class', 'border ps-2');
        const blockquote = document.createElement('blockquote');
        blockquote.setAttribute('class', 'blockquote');
        const info = document.createElement('p');
        info.textContent = this.getAttribute('data-comment-message');
        const figcaption = document.createElement('figcaption');
        figcaption.setAttribute('class', 'blockquote-footer');
        const cite = document.createElement('cite');
        const author = this.getAttribute('data-comment-author');
        cite.setAttribute('title', author);
        cite.textContent = author;
        shadow.appendChild(linkElem);
        shadow.appendChild(figure);
        figure.appendChild(blockquote);
        figure.appendChild(figcaption);
        blockquote.appendChild(info);
        figcaption.append('Written by ', cite);
    }
}

customElements.define('custom-quote', Quote);