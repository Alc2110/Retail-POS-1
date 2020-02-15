--Staff
INSERT INTO Staff (FullName, PasswordHash, Privelege)
VALUES ('user a', '1262301944193641595511921623780187391981092451687821318915315425151391968922796136', 'Admin');

INSERT INTO Staff (FullName, PasswordHash, Privelege)
VALUES ('user n', '1771215959170212241201912419720863234153588222241158114225160147450620532073745', 'Normal');

--Customers
INSERT INTO Customers (FullName, StreetAddress, PhoneNumber, Email, City, State_, Postcode)
VALUES ('John Smith', '1 Example St', '123456', 'johnsmith@email.com', 'Somewhere', 'NSW', 2000);

INSERT INTO Customers (FullName, StreetAddress, PhoneNumber, Email, City, State_, Postcode)
VALUES ('Steve Simpson', '12 Demonstration Cr', '654321', 'stevesimpson@email.com', 'Elsewhere', 'Vic', 3000);

--Products
INSERT INTO Products (ProductIDNumber, Description_, Quantity, Price)
VALUES ('9300830002399', 'Deodorant Rexona Men MotionSense Xtra Cool', 100, 4.95);

INSERT INTO Products(ProductIDNumber, Description_, Quantity, Price)
VALUES ('9310059009031', 'Mens Regaine Extra Strength Solution Hair Regrowth Treatment', 50, 50);

INSERT INTO Products(ProductIDNumber, Description_, Quantity, Price)
VALUES ('9325193001001', 'Bushmaster Quality Compact Armored Binoculars', 20, 15.95);

INSERT INTO Products(ProductIDNumber, Description_, Quantity, Price)
VALUES ('9781491987650', 'OReilly C# 7.0 in a Nutshell The Definitive Reference', 25, 60);

INSERT INTO Products(ProductIDNumber, Description_, Quantity, Price)
VALUES ('66782001103', 'Toppik Hair Building Fibres', 25, 25.95);
