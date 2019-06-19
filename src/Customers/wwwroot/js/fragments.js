/* eslint-disable no-use-before-define, no-console, class-methods-use-this */
/* globals HTMLElement, window, CustomEvent */
(function fragments() {
    class CustomerProfileCard extends HTMLElement {
        static get observedAttributes() {
            return ['customerId'];
        }
        connectedCallback() {
            const customerId = this.getAttribute('customerId');
            this.log('connected', customerId);
            this.render();
        }
        render() {
            const customerId = this.getAttribute('customerId');
            var that = this;
            
            $.get("http://localhost:5004/profiles/" + customerId, function(data) {
                
                that.innerHTML = data;
            });
        }
        attributeChangedCallback(attr, oldValue, newValue) {
            this.log('attributeChanged', attr, oldValue, newValue);
            this.render();
        }
        disconnectedCallback() {
            const customerId = this.getAttribute('customerId');
            this.log('disconnected', customerId);
        }
        log(...args) {
        console.log('🔘customer-profile-card', ...args);
    }
}
    window.customElements.define('customer-profile-card', CustomerProfileCard);

}());