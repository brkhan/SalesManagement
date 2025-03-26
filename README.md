Sales management

# Instructions to start application
1. Clone the repository
2. Chose develop branch
3. Start the application. This should start two applications, SalesManagement.UI and SalesManagement.API
4. SalesManagement.UI is a Blazor application, has user interface for chosing summary or all records from csv.
5. SalesManagement.API has endpoints for getting summary and all records from csv. Its a REST api.

## Observations
1. Record(699) has no manfacturing price. This is ignored.  Up for discussion.
Government,	Mexico,	 Montana, 	 High, 	1368,	, ,	£7.00,	01/02/2014

2. Record(57) has invalid date. This is ignored.
 Government,	France,	 Paseo, 	 Low, 	1030,	£10.00,	£7.00,	01/13/2014

3. Record(681) has no Product. This is accepted as with no Product. Up for discussion.
 Government,	Mexico,		,,	2151,	£10.00,	£350.00,	01/11/2013

4.  Record(146) has an extra space before decimal. This is accepted as 4243.5
Enterprise	France	 Carretera 	 Low 	4243 .5	£3.00	£125.00	01/04/2014

5. File encoding is not changed- file is not updated in any way



