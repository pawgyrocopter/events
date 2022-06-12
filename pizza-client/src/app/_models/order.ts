import { Pizza } from "./pizza"

export interface Order{
  name: string
  orderId : number
  pizzas: Pizza[]
  orderState : OrderState
  orderStateAsString: string
}

export enum OrderState{
  Making,
  Ready
}
