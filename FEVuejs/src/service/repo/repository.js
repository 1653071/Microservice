import axios from 'axios';

const baseDomain = 'https://localhost:44334';
export const getUser = ()=>{
    const currentUser = JSON.parse(localStorage.getItem("currentUser"));
    return currentUser;
}
export const currentUser = JSON.parse(localStorage.getItem("currentUser"));
const token= localStorage.getItem("accessToken")
const instance = axios.create({
    baseURL:baseDomain,
    headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`
    }
});
export const Color = [{id:0,color:"#CC0066"},{id:1,color:"#CC00CC"},{id:2,color:"#0080ff"},{id:3,color:"#ffff33"}]

export default instance;