import {Cart} from "./cart";

export interface User{
  name: string;
  token: string;
  roles: string[];
  cart : Cart;
}
