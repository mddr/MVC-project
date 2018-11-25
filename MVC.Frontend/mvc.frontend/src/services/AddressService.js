import AuthService from "./AuthService";

export class AddressService {
    constructor() {
        this.Auth = new AuthService();
        this.add = this.add.bind(this)

    }

    add(city, postalCode, street, houseNumber) {
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

    userAddress() {
        return this.Auth.fetch(`${this.Auth.domain}/userAddress`, {
            method: 'get',
        });
    }

    addresses() {
        return this.Auth.fetch(`${this.Auth.domain}/addresses`, {
            method: 'get',
        });
    }

    address(id) {
        return this.Auth.fetch(`${this.Auth.domain}/address/${id}`, {
            method: 'get',
        });
    }

    update(id) {
        return this.Auth.fetch(`${this.Auth.domain}/address/update/${id}`, {
            method: 'post',
        });
    }

    delete(id) {
        return this.Auth.fetch(`${this.Auth.domain}/address/delete/${id}`, {
            method: 'delete',
        });
    }

}
export default AddressService;
