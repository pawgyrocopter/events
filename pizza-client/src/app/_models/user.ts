import {Cart} from "./cart";

export interface User{
  userName: string;
  token: string;
  roles: string[];
  cart : Cart;
}
