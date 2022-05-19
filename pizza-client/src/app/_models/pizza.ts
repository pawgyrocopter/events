import {Photo} from "./photo";
import {Toping} from "./toping";

export interface Pizza {
  id: number
  name: string
  photo : Photo
  cost : number
  weight : number
  ingredients : string
  state : number
  topings : Toping[]
}
