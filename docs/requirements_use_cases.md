# Requirements and use cases (< v0.9)
## Use cases
### Log in (COMPLETE)
1. User supplies login credentials, via the login dialog box.
2. Credentials compared to stored credentials.

#### Variation #1:
1.1 Start at step 2.

1.2 Credentials match. User authenticated. Main window displayed.

### Variation #2:
2.1 Start at step 2.

2.2 Credentials do not match. Error box displayed. Login window displayed again.


### Log out
1. User clicks logout button or File->logout.
2. "Are you sure?" dialog displayed. Could be in the middle of a sale.
3. Main window closed, login dialog box displayed.


### Create new staff profile (Admin only)
1. File->Add new staff clicked (greyed out unless current user is admin).
2. Dialog box displayed.
3. Staff data entered. 
3. Attempt connection to database, send data.
4. Confirmation if successful, otherwise error message displayed. 


### Create new customer profile
Similar to create new staff profile, except any staff member can do this.


### View customer list
1. User clicks "Customer List" button
2. Attempt connection to database, retrieve data.
3. Form with read-only list of customer data displayed if successful, otherwise error message displayed.


### Initiate new sale
1. User clicks one of the new sale buttons.
### Variation #1:
1.1 Start at step 1. Customer is a member. "Find" button enabled.

1.2 Customer account number textbox enabled. Account number entered. "Find" button clicked.

1.3 Request sent to database to find customer data. If successful, display it in the "Customer details" group, "Find" button disabled, both "new sale" buttons disabled. Otherwise, show an error message, and reset the UI.

### Variation #2 (DONE):
2.1 Start at step 1. Customer is not a member.

2.1 Both "new sale" buttons disabled. "Add item" button enabled. "Clear sale" button enabled.


### Add item to checkout list.
1. Product ID entered.
2. Click "Add Item" button. Attempt to retrieve information from database. If there is an error, tell the user. Otherwise, continue.
3. Display item information in the list.


### Remove item from checkout list.
1. Item selected in list.
2. "Remove Item" button clicked. Item removed from list. Price removed. If no more items, disable this button.


## Clear sale (PARTIALLY IMPLEMENTED)
1. "Clear sale" button clicked.
2. If there are no items in the list, reset the UI. Else, show confirmation dialog.
3. If the user confirms, clear the items, reset the UI.


### Checkout 
1. Checkout clicked
2. For all items in the list, transaction is performed in the database. If there is an error, inform the user, reset the UI. Otherwise, continue.
3. Confirmation. 
4. Reset the UI.


### Look up item.


### Add new item (Admin only)
1. "New Item" button clicked.
2. Dialog displayed.
3. Data entered.
4. "Ok" button clicked.
5. Confirmation.


### Look up transactions (Admin only)


### Generate report