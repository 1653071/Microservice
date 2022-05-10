import repository from "../repo/repository"
export default {
   getAllUserInGroup(groupid){
    return repository.get(`/userpermission/getUserInGroup/${groupid}`)
   }
}