CREATE DATABASE SensorProject;
USE SensorProject;

CREATE TABLE Sensors (
    SensorID INT PRIMARY KEY AUTO_INCREMENT,
    SensorName VARCHAR(50) NOT NULL,
    IsActive BOOLEAN DEFAULT TRUE,
    Uses INT DEFAULT 0,
    HasMatched BOOLEAN DEFAULT FALSE
);

CREATE TABLE Agents (
    AgentID INT PRIMARY KEY AUTO_INCREMENT,
    AgentType VARCHAR(50) NOT NULL,
    Weaknesses TEXT NOT NULL,
    SensorAmount INT NOT NULL
);

CREATE TABLE MatchedWeaknesses (
    MatchID INT PRIMARY KEY AUTO_INCREMENT,
    SensorID INT,
    AgentID INT,
    WeaknessCount INT DEFAULT 0,
    FOREIGN KEY (SensorID) REFERENCES Sensors(SensorID),
    FOREIGN KEY (AgentID) REFERENCES Agents(AgentID)
);

