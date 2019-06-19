/* eslint-disable no-use-before-define, no-console, class-methods-use-this */
/* globals HTMLElement, window, CustomEvent */
(function fragments() {
    const state = {
        name: "Spongebob Squarepants"
    };
    
    class AuthorizationUserProfile extends HTMLElement {
        connectedCallback() {
            this.refresh = this.refresh.bind(this);
            this.log('connected');
            this.render();
            window.addEventListener('authorization:userprofile:changed', this.refresh);
        }
        refresh() {
            this.log('event received "authorization:userprofile:changed"');
            this.render();
        }
        render() {
            this.innerHTML = `
        <div style="background-color: red">${state.name}</div>
      `;
        }
        disconnectedCallback() {
            window.removeEventListener('authorization:userprofile:changed', this.refresh);
            this.log('disconnected');
        }
        log(...args) {
        console.log('🛒authorization-user-profile', ...args);
    }
}
    window.customElements.define('authorization-user-profile', AuthorizationUserProfile);

}());