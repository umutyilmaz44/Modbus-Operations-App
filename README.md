# Modbus Operations App
It is a sample application for reading and writing operations via Modbus TCP protocol.

It is an application that can read and write data from a dynamically defined target using Modbus TCP protocol.

Also, converting the raw data received from the relevant address to the targeted data type can be performed.

It can make modbus writing operations by converting the data entered from the screen into the related register array.

In addition, it has an automatic reading feature in the desired period.

It is also possible to save the defined records in json format and load the list over registered json.

Writing is only active for Holding Register and Coil. Values entered in the Value field by selecting the IsWritable property will be written to the modbus device. You can use the Write button for this process.

Data lengths are determined automatically for other types except String and cannot be changed. However, you must specify the data length when the data type is String.

[Screenshot]
![modbus-app](https://user-images.githubusercontent.com/42136540/85747005-932fd780-b70f-11ea-9e45-99756f2f6545.PNG)
