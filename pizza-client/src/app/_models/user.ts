import {Basket} from "./basket";

export interface User{
  userName: string;
  token: string;
  roles: string[];
  basket : Basket;
}
