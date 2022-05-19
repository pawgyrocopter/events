import {Photo} from "./photo";

export interface Pizza {
  id: number
  name: string
  photo : Photo
  cost : number
  weight : number
  ingredients : string
  state : number
}
