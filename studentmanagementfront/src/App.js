import React from 'react';
import { Container, Row, Col } from 'reactstrap';
import { BrowserRouter as Router, Route, Routes, Link } from 'react-router-dom';
import StudentList from './components/StudentList';
import StudentDetails from './components/StudentDetails';
import BestStudentsList from './components/BestStudentsList'; // Importando o novo componente

const App = () => {
  return (
    <Router>
      <Container>
        <Row className="my-4">
          <Col>
            <h1>Lista de Alunos</h1>
          </Col>
        </Row>
        
        {/* Lista de melhores alunos */}
        <Row>
          <Col>
            <h3>Melhores Alunos por Matéria</h3>
            <BestStudentsList />
          </Col>
        </Row>

        <Row>
          <Col md="6">
            <StudentList />
          </Col>
          <Col md="6">
            <h3>Detalhes do Aluno</h3>
            <Routes>
              {/* Detalhes do aluno */}
              <Route path="/student/:registration" element={<StudentDetails />} />  
            </Routes>
          </Col>
        </Row>
      </Container>
    </Router>
  );
};

export default App;
