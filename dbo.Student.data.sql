SET IDENTITY_INSERT [dbo].[Student] ON
INSERT INTO [dbo].[Student] ([Id], [DOB], [Citizenshipnumber], [Fathersname], [Mothersname], [Occupation], [Name], [Contact], [Email], [Address]) VALUES (NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Student] OFF
ALTER TABLE dbo.Student
ADD ProfileImagePath varchar(255);