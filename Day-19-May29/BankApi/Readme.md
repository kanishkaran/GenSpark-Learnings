# BankApi

A simple banking API built with ASP.NET Core, supporting account management, transactions, and money transfers.

## Services Overview

- **AccountService**  
  Handles account creation, viewing, listing, and deletion (deactivation).

- **TransactionService**  
  Supports deposit and withdrawal operations for accounts.

- **TransferAmountService**  
  Manages transferring money between accounts, ensuring transactional integrity.

## API Endpoints

### Account Endpoints

- `POST /api/Account`  
  **Create Account**  
  Request body: `AccountAddRequestDto`  
  Response: Created account details

- `GET /api/Account/Id?id={id}`  
  **Get Account By Id**  
  Response: Account details

- `GET /api/Account`  
  **View All Accounts**  
  Response: List of all accounts

- `DELETE /api/Account?id={id}`  
  **Delete (Deactivate) Account**  
  Response: Deactivated account details

### Transaction Endpoints

- `POST /api/Transaction/deposit`  
  **Deposit Money**  
  Request body: `{ accountNumber, amount }`  
  Response: Transaction entry

- `POST /api/Transaction/withdraw`  
  **Withdraw Money**  
  Request body: `{ accountNumber, amount }`  
  Response: Transaction entry

### Transfer Endpoints

- `POST /api/Transaction/Transfer`  
  **Transfer Money Between Accounts**  
  Request body: `{ fromAccount, toAccount, amount }`  
  Response: List of transaction entries for both accounts

## Notes

- All endpoints return appropriate error messages for invalid operations (e.g., insufficient balance, deactivated accounts).

---

For more details, see the source code in the [Controllers](Controllers/) and [Services](Services/) directories.