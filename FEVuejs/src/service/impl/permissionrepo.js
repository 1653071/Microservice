import repository from "../repo/repository"
export default {
   getAllPermissionByUser(id){
    return repository.get(`/permission/getAllPermissionByUser/${id}`)
   },
   getAllGroup(){
      return repository.get("/group")
   },
   addGroup(data){
     return repository.post(`group`, data)
   },
   getUserNotInGroup(id){
      return repository.get(`userpermission/getUserNotInGroup/${id}`)
   },
   getUserInGroup(id){
      return repository.get(`/userpermission/getUserInGroup/${id}`)
   },
   getAllPermissionByGroupId(id){
      return repository.get(`/permission/getPerMissionByGroupId/${id}`)
   },
   getAllPermissionNotInByGroupId(id){
      return repository.get(`/permission/getPerMissionNotInGroup/${id}`)
   },
   getAllUserInGroup(groupid){
      return repository.get(`/userpermission/getUserInGroup/${groupid}`);
   },
   addPermissionToGroup(data){
      return  repository
      .post(`grouppermission`, data)

   },
   removePermission(data){
      return repository
      .post(`/grouppermission/removeGroupPermission`, data)
   },
   removeUser(data){
      return repository.post(`userpermission/remove`, data);
   },
   insertUser(data){
      return repository.post(`userpermission/insert`, data)
   }
}