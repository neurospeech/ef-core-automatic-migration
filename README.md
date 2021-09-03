# Automatic Migration support for EF Core
Entity Framework Core does not support automatic migration like earlier EntityFramework 6.x; Though it is useful feature, looks like it may not be implemented due to other priorities.

This library helps in running automatic migration against database with following features.

# Features

1. Creates Missing Table
2. Creates Missing Columns
3. Renames old column with same name, creates new column and migrates data if no loss occurs, you can customize this.
4. Renames old indiexes and creates new ones
5. Creates indexes based on foreign keys
6. Entire migration runs in a single transaction, so if changes are large, they will fail and rolled back to previous state.

# Warning

1. Please test this thoughly before using it in production, always take backup of your database before running migrations on production.
2. It is also recommended to run migrations separately with some conditions, so you are aware of what changes are being taken.
3. This library is essential in earlier stages of development, and should only be used for small changes like adding non existing tables/columns.

# Installation

```
	PM> Install-Package NeuroSpeech.EFCoreAutomaticMigration
```

# Providers

Currently two providers, SQL Server and PostGreSql are supported by this library.

## Sql Server
```
	PM> Install-Package NeuroSpeech.EFCoreAutomaticMigration
```
## PostGreSql Server

# ASP.NET Core
```
	PM> Install-Package NeuroSpeech.EFCoreAutomaticMigration.PostGreSql
```

```c#

public void Configure(IApplicationBuilder app, IHostingEnvironment env){

	if(env.IsDevelopment()){
		app.UseDeveloperExceptionPage();

		using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
		{
			using (var db = scope.ServiceProvider.GetRequiredService<AppModelContext>())
			{
				db.MigrationForSqlServer()
					.Migrate(preventChangeColumn: true);
			}
		}
	}

}

```

# Change in existing Columns

Many times even slight change in model may result in changing of column schema, though there are two options in such case.

## Prevent Change 

This is the default option, in this case, the migration will fail.

## Manually Apply the Change

Lets assume we have made BrokerID optional and it needs to be updated as nullable in the schema. In this case we can run a simple alert statement manually to make migration smooth. This will happen only if the target schema is old. The condition will not execute if target contains exact same schema.

```c#
	db.MigrationForSqlServer()
	    .OnColumnChange((migration, existingColumn, currentColumn, tempName) => {
		     // you can use properties to run some migrations manually ...
			 if(existingColumn.TableNameAndColumnName == "[Product].[BrokerID]"
			     && existingColumn.IsNullable != currentColumn.IsNullable) {
			     // since we just changed column to nullable.. we can simpley run alter by ourselves...
				 migration.Run("ALTER TABLE [Product] ALTER COLUMN [BrokerID] INT NULL");
			 }
		})
		.Migrate();
	
```


