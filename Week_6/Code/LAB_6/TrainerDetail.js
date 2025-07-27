import React from 'react';
import { useParams } from 'react-router-dom';
import trainersMock from './trainersMock';

const TrainerDetail = () => {
  const { id } = useParams();
  const trainer = trainersMock.find((t) => t.trainerId === id);

  if (!trainer) return <p>Trainer not found</p>;

  return (
    <div>
      <h2>Trainers Details</h2>
      <strong>{trainer.name} ({trainer.technology})</strong>
      <p>{trainer.email}</p>
      <p>{trainer.phone}</p>
      <ul>
        {trainer.skills.map((skill, i) => (
          <li key={i}>{skill}</li>
        ))}
      </ul>
    </div>
  );
};

export default TrainerDetail;
