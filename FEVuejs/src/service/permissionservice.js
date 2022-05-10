import instance from "./repo/repository";
import { Role } from "./role/role";
export const getPermission = async () => {
  const usid = JSON.parse(localStorage.getItem("currentUser")).id;
  let list = [];
 
  await instance
    .get(`/permission/getAllPermissionByUser/${usid}`)
    .then(res => {
      list = res.data.data;
      
    })
    .catch(e => {});
    return list;
 
};
export const checkPermission = async (listPermission,permission) => {
   let list_check = [];
  
    list_check =listPermission.filter(e=>{
      return permission.includes(e.perid) ;
    })
 
   if(list_check.length == 0 ){
     return false;
   }
   return true;
};


