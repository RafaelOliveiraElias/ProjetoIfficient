import axios from 'axios';
import config from '../config'; // Importando a configuração para pegar a URL da API

// Criando a instância do axios com a URL base vindo da configuração
const api = axios.create({
  baseURL: `${config.apiUrl}/student`, // Usando a URL configurada
  headers: {
    'Content-Type': 'application/json',
  },
});

// Função para obter alunos ordenados
export const getSortedStudents = (strategy) => {
  return api.get(`/sorted?strategy=${strategy}`);
};

export const getStudentByRegistration = (registration) => {
  return api.get(`/${registration}`);
};

export const getBestStudentsBySubject = () => {
  return api.get('/best-by-subject');
};


export default api;


