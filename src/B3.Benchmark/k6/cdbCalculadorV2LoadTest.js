import http from 'k6/http'

const params = {
   headers: {
     'Content-Type': 'application/json',
   },
 };

export const options = {
   
   insecureSkipTLSVerify: true,
   noConnectionReuse: true,
   vus: 1,
   duration: '10s',
};

export default () => {  

   let investment = JSON.stringify({
      "investment": {
         "initialValue": 1000,
         "redeemTermMonths": getRndInteger(2, 60)
      }
   })  

   http.post('http://localhost:5138/cdbfunctional', investment, params);      
};


function getRndInteger(min, max) {
   return Math.floor(Math.random() * (max - min + 1)) + min;
}


