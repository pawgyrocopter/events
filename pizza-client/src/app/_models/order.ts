import { Pizza } from "./pizza"

export interface Order{
  name: string
  orderId : number
  pizzas: Pizza[]
  orderState : OrderState
}

export enum OrderState{
  Making,
  Ready
}
