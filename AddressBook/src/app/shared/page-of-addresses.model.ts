import { Address } from "./address.model";

export class PageOfAddresses {
    items: Address[];
    noOfPages : number;
    currentPage: number;
}
