import AuthService from "./AuthService";

export class AddressService {
    constructor() {
        this.Auth = new AuthService();
        this.add = this.add.bind(this)
        this.addresses = this.addresses.bind(this)
        this.address = this.address.bind(this)
        this.auserAddressdd = this.userAddress.bind(this)
        this.update = this.update.bind(this)
        this.delete = this.delete.bind(this)

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

    userAddress() {
        return this.Auth.fetch(`${this.Auth.domain}/userAddress`, {
            method: 'get',
        });
    }

    update() {
        return this.Auth.fetch(`${this.Auth.domain}/address/update/`, {
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
