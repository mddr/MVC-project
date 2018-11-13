import AuthService from "./AuthService";

export class AddressService {
    constructor() {
        this.Auth = new AuthService();
        this.addAddres = this.addAddres.bind(this)

    }

    addAddres(city, postalCode, street, houseNumber) {
        let body = "";
        const obj = {
            city: city,
            postalCode: postalCode,
            street: street,
            houseNumber: houseNumber,
        };

        body = JSON.stringify(obj);

        return this.Auth.fetch(`${this.Auth.domain}/address/add`, {
            method: 'post',
            body
        });
    }

}
export default AddressService;
