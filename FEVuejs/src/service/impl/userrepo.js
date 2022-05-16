
import repository from "../repo/repository"
export default {
   getAllUser(){
    return repository.get("/user/list-user");
   },
   deleteUser(user){
    return repository.delete(`/user/list-user/delete`, { data: user });
   },
   createUser(user){
     return repository.post(`https://localhost:44334/user/list-user/create`, user)
   },
   updateUser(user){
      return repository.put(`/user/list-user/update`, user)
   }
}
