import styles from './components/CohortDetails.module.css';
import React from 'react';

function CohortDetails(props) {
    const { cohort } = props;

    console.log("Current Status:", cohort.currentStatus); 
    const status = cohort.currentStatus.trim().toLowerCase();
    const headingClass = status === "ongoing" ? styles.green : styles.blue;

    return (
        <div className={styles.box}>
            <h3 className={headingClass}>
                {cohort.cohortCode} - <span>{cohort.technology}</span>
            </h3>
            <dl>
                <dt>Started On</dt>
                <dd>{cohort.startDate}</dd>
                <dt>Current Status</dt>
                <dd>{cohort.currentStatus}</dd>
                <dt>Coach</dt>
                <dd>{cohort.coachName}</dd>
                <dt>Trainer</dt>
                <dd>{cohort.trainerName}</dd>
            </dl>
        </div>
    );
}

export default CohortDetails;
