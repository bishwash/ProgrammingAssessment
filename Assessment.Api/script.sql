IF OBJECT_ID(N'__EFMigrationsHistory') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190319032725_AddInitialAssessmentTable')
BEGIN
    CREATE TABLE [Student] (
        [Id] uniqueidentifier NOT NULL,
        [DOB] date NULL,
        [FirstName] nvarchar(256) NOT NULL,
        [GPA] decimal(3, 2) NOT NULL,
        [LastName] nvarchar(256) NOT NULL,
        CONSTRAINT [PK_Student] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190319032725_AddInitialAssessmentTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190319032725_AddInitialAssessmentTable', N'2.0.3-rtm-10026');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190319034907_UpdateDOBToNotNullable')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'Student') AND [c].[name] = N'DOB');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Student] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Student] ALTER COLUMN [DOB] date NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190319034907_UpdateDOBToNotNullable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190319034907_UpdateDOBToNotNullable', N'2.0.3-rtm-10026');
END;

GO

