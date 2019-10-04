How to re-generate EntityFrameworkCore context from existing Database
		- DB Project [Finanace.Database]

---------------------------------------------------------------------------------------------------------------------------------------
Dependancies:

Microsoft.EntityFrameworkCore				https://www.nuget.org/packages/Microsoft.EntityFrameworkCore/3.0.0?_src=template
Microsoft.EntityFrameworkCore.SqlServer		https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer/3.0.0?_src=template
Microsoft.EntityFrameworkCore.Tools			https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Tools/3.0.0?_src=template

---------------------------------------------------------------------------------------------------------------------------------------
VS: set startup project to Finanace.DataAccess
---------------------------------------------------------------------------------------------------------------------------------------
Package Manager Console Host Version 5.3.0.6251
	[Default Project: Finanace.DataAccess]

Run this command in the console window:
Scaffold-DbContext "Server=2016DBServer;Database=Finance;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
