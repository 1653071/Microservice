import repository from "../repo/repository"
export default {
   getAllPermissionByUser(id){
    return repository.get(`/permission/getAllPermissionByUser/${id}`)
   }
}