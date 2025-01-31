

## Pharmacy POS System 
### Backend (ASP.NET Core 6 Web API)

This is the **ASP.NET Core 6 Web API** backend for the **Pharmacy POS System**. It provides API endpoints for managing **Brands, Categories, Products, Cart, and Orders**.

---

## **Features**
- 📌 **Product Management**: CRUD operations for Brands, Categories, and Products.
- 🛒 **Cart Management**: Uses **Session** for storing cart data.
- 💳 **Order Processing**: Saves order details with selected payment method.
- 🔗 **RESTful API**: Secure and optimized endpoints for the Angular frontend.
- 🛠 **CORS Enabled**: Supports cross-origin requests.

---

## **Installation & Setup**
### **1. Clone the Repository**
```sh
git clone https://github.com/aminul-islam-niloy/Pharmacy_POS_System_API.git
```
Setup ConnectionStrings and update database.

### **2. Prerequisites**
Ensure you have the following installed:
- [.NET SDK 6.0+](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Visual Studio Code or Visual Studio](https://visualstudio.microsoft.com/)

```
cd Pharmacy_POS_System_API
```
### **3. Run Project**
```
dotnet run
```


### **4. Access Swagger UI:**
   Open your browser and navigate to:
   ```
   http://localhost:7083/swagger/index.html
   ```

---

## API Endpoints

### **Brand**
| Method | Endpoint          | Description           |
|--------|------------------|-----------------------|
| GET    | `/api/Brand`     | Get all brands       |
| POST   | `/api/Brand`     | Create a new brand   |
| DELETE | `/api/Brand/{id}` | Delete a brand by ID |

### **Cart**
| Method  | Endpoint                          | Description                       |
|---------|----------------------------------|-----------------------------------|
| GET     | `/api/Cart`                      | Get cart details                 |
| POST    | `/api/Cart/AddToCart`            | Add a product to the cart        |
| DELETE  | `/api/Cart/RemovefromCart/{productId}` | Remove a product from the cart |

### **Category**
| Method  | Endpoint           | Description                 |
|---------|-------------------|-----------------------------|
| GET     | `/api/Category`    | Get all categories         |
| POST    | `/api/Category`    | Create a new category     |
| DELETE  | `/api/Category/{id}` | Delete a category by ID |

### **Order**
| Method | Endpoint                      | Description                  |
|--------|------------------------------|------------------------------|
| POST   | `/api/Order/save-order`       | Save an order               |
| GET    | `/api/Order/get-all-orders`   | Retrieve all orders         |

### **Product**
| Method  | Endpoint                                 | Description                                 |
|---------|-----------------------------------------|---------------------------------------------|
| GET     | `/api/Product`                          | Get all products                           |
| POST    | `/api/Product`                          | Add a new product                          |
| GET     | `/api/Product/category/{categoryId}`   | Get products by category ID                |
| DELETE  | `/api/Product/{id}`                     | Delete a product by ID                     |
| GET     | `/api/Product/{id}/image`               | Get product image by ID                    |

---

## Technologies Used
- **ASP.NET Core 6**
- **Entity Framework Core** (for database operations)
- **SQL Server**
- **Swagger UI** (for API testing)

## License
This project is licensed under the MIT License.

---

For the **Angular 18 Frontend**, refer to its repository:
[Pharmacy POS System UI](https://github.com/aminul-islam-niloy/Pharmacy_POS_System-UI)

