Insert into TipoUsuario (Tipo) values ('Administrador')
GO
Insert into TipoUsuario (Tipo) values ('Desenvolvedor')
GO
Insert into TipoUsuario (Tipo) values ('Médico')
GO
Insert into TipoUsuario (Tipo) values ('Paciente')
GO

Insert into Usuario (Nome, Email, Senha, IdTipoUsuario)
Values ('Fernanda', 'fernanda@gmail.com', '$2b$10$EI1ZWRc5UKA1/MSDMieQ7OGcEZOww.usCcuSPh8Al36R.B7FP12.y', (Select id From TipoUsuario Where Tipo = 'Administrador'))
GO