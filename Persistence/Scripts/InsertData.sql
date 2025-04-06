INSERT INTO Project (Id, [Name], Description, Security) 
VALUES 
('12345', 'Proyecto Alpha', 'Este es un proyecto de prueba', 5),
('12346', 'Proyecto Beta', 'Otro proyecto interesante', 3),
('12347', 'Proyecto Gamma', NULL, 2), -- Sin descripci�n (NULL)
('12348', 'Proyecto Delta', 'Proyecto avanzado', 4);

INSERT INTO [User] (Id, Name, Email, Password, Phone, ProjectId, CreatedDate, UpdatedDate, Active) 
VALUES 
('U12345', 'Juan P�rez', 'juan@example.com', 'hashedpassword123', '555-1234', '12345', GETDATE(), GETDATE(), 1),
('U12346', 'Mar�a L�pez', 'maria@example.com', 'hashedpassword456', '555-5678', '12346', GETDATE(), GETDATE(), 1),
('U12347', 'Carlos Ruiz', 'carlos@example.com', 'hashedpassword789', NULL, '12347', GETDATE(), GETDATE(), 0), -- Sin tel�fono
('U12348', 'Ana Torres', 'ana@example.com', 'hashedpassword000', '555-9999', '12348', GETDATE(), GETDATE(), 1);

INSERT INTO Person (Id, Name, LastName, UserId) 
VALUES 
('P12345', 'Carlos', 'G�mez', 'U12345'),
('P12346', 'Laura', 'Mart�nez', 'U12346'),
('P12347', 'Pedro', 'S�nchez', 'U12347'),
('P12348', 'Ana', 'P�rez', 'U12348');
