import { LayoutPlugin } from "bootstrap-vue"
import repository from "../repo/repository"
export default {
  
   login(data){
       return repository.post(`https://localhost:44334/login`, data)
   }
}