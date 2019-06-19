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
            
            $.get("http://localhost:5004/" + customerId, function(data) {
                
                that.innerHTML = `<div class="card">
                                    <div class="card-header">Customer information</div>
                                    <div class="card-body">
                                        <strong>Name:</strong> ${data.name}<br />
                                        <strong>Phone:</strong> ${data.phone}<br />
                                        <strong>Email:</strong> <a href="mailto:${data.email}">${data.email}</a><br />
                                    </div>
                                  </div>`;
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