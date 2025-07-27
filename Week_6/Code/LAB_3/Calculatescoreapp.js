import React, { useState } from 'react';

const CalculateScore = () => {
  const [name, setName] = useState('');
  const [school, setSchool] = useState('');
  const [total, setTotal] = useState('');
  const [goal, setGoal] = useState('');
  const [show, setShow] = useState(false);

  const handleClick = () => {
    setShow(true);
  };

  const calcScore = () => {
    const avg = (Number(total) / 5).toFixed(2);
    return `Average: ${avg} | Goal: ${goal}`;
  };

  const containerStyle = {
    fontFamily: 'Arial',
    padding: '20px',
    maxWidth: '450px',
    margin: '30px auto',
    border: '2px solid #ccc',
    borderRadius: '10px',
    backgroundColor: '#f0f0f0'
  };

  const headingStyle = {
    color: 'brown',
    textAlign: 'center'
  };

  const inputStyle = {
    width: '100%',
    padding: '8px',
    margin: '8px 0',
    borderRadius: '4px',
    border: '1px solid #ccc'
  };

  const buttonStyle = {
    backgroundColor: '#4CAF50',
    color: 'white',
    padding: '10px',
    border: 'none',
    width: '100%',
    borderRadius: '5px',
    cursor: 'pointer'
  };

  const resultStyle = {
    marginTop: '20px',
    padding: '10px',
    backgroundColor: '#fff',
    borderRadius: '6px'
  };

  return (
    <div style={containerStyle}>
      <h2 style={headingStyle}>Student Score Calculator</h2>

      <input
        type="text"
        placeholder="Enter Name"
        value={name}
        onChange={(e) => setName(e.target.value)}
        style={inputStyle}
      />

      <input
        type="text"
        placeholder="Enter School"
        value={school}
        onChange={(e) => setSchool(e.target.value)}
        style={inputStyle}
      />

      <input
        type="number"
        placeholder="Enter Total Marks"
        value={total}
        onChange={(e) => setTotal(e.target.value)}
        style={inputStyle}
      />

      <input
        type="number"
        placeholder="Enter Goal"
        value={goal}
        onChange={(e) => setGoal(e.target.value)}
        style={inputStyle}
      />

      <button onClick={handleClick} style={buttonStyle}>
        Calculate Average
      </button>

      {show && (
        <div style={resultStyle}>
          <p><strong>Name:</strong> {name}</p>
          <p><strong>School:</strong> {school}</p>
          <p><strong>Total:</strong> {total} Marks</p>
          <p><strong>{calcScore()}</strong></p>
        </div>
      )}
    </div>
  );
};

export default CalculateScore;
