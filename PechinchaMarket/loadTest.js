import http from 'k6/http';

export const options = {
    vus: 2000,
    duration: '10s', // quanto tempo o usuario vai fazer requisições
    thresholds: {
        http_req_failed: ['rate<0.15'], // http errors should be less than 1%
        http_req_duration: ['p(95)<7000'], // 95% of requests should be below 200ms
    },
};

export default function () {
  
    http.post('https://pechinchamarket.azurewebsites.net');

}