# Introduction

## 1.1. Background and Context

In the healthcare sector, monitoring patients' vital signs on a regular basis is essential to detect health problems early and provide better care. Key measurements such as blood pressure, pulse rate, weight, and height give healthcare professionals valuable information about a patient's condition over time. However, in many small health centres, these records are still managed using paper forms or disconnected digital tools, which makes it difficult to keep a complete and up-to-date history for each patient.

This situation often leads to delays in identifying abnormal values, poor communication between patients and their healthcare workers, and administrative inefficiencies in managing appointments and user data. Furthermore, when patients cannot access their own health records easily, they are less likely to be actively involved in monitoring their own condition.

The GOsasun project was developed to address these problems by creating a unified digital platform that connects all the people involved in a health centre: patients, healthcare workers, and receptionists. The system was built using web technologies (PHP, HTML, CSS, and jQuery) together with a C# desktop application, and it runs on a MySQL relational database.

## 1.2. Objectives

The main objective of this project is to design and develop a multiplatform healthcare management application centred on patients and their clinical follow-ups.

The specific objectives of the project are:

- To allow patients to record their own vital signs (blood pressure, pulse, weight, and height) and view their follow-up history with graphical charts.
- To enable healthcare workers to manage their assigned patients' follow-ups, attach medical documents to each record, write prescriptions, and schedule appointments.
- To provide receptionists with administrative tools for managing all users (patients, healthcare workers, and other receptionists), overseeing appointments, and handling internal and external messages.
- To integrate the C# desktop application with real tensiometers so that blood pressure and pulse data can be captured automatically and exchanged using XML format.
- To implement a secure relational database with views, triggers, and indexes that ensures data integrity and efficient access.
- To support multilingual configuration (Basque, Spanish, and English) and visual customisation through XML configuration files.

## 1.3. Scope and Approach

The project covers the full development of two complementary platforms. The web application, hosted on an Apache server, provides role-based access through a login system. Each role has its own portal with specific features:

- **Patients** can enter their measurements, consult their follow-up history in a paginated table, upload documents linked to each follow-up, view graphical charts of their data, request appointments, and communicate through an internal messaging system.
- **Healthcare workers** can view and edit the follow-ups of their assigned patients, attach diagnostic documents (such as test results or clinical reports) to specific follow-up records, manage prescriptions, schedule appointments, receive clinical alerts when abnormal values are detected, and export patient data in XML format.
- **Receptionists** act as the administrative centre of the application. They can create, edit, and delete all types of users, manage the appointment calendar for the entire centre, handle both internal messages and external contact requests, and configure the global settings of the platform.

The desktop application, developed in C#, connects with Beurer BM58 tensiometers to read blood pressure and pulse measurements directly from the device. This data is processed and stored in XML files, which can then be imported into the web platform.

The database architecture uses MySQL with advanced features such as SQL views for simplified and secure queries, triggers for automatic data updates, foreign key constraints for referential integrity, and indexes for query optimisation.

## 1.4. Expected Outcome

The expected result of this project is a fully functional healthcare portal that places the patient and their clinical follow-ups at the centre of the system. The platform is expected to improve patient self-monitoring by providing clear access to their own health data, facilitate the work of healthcare professionals by centralising follow-up records and attached documents, and reduce administrative workload through automated user and appointment management. Overall, GOsasun aims to create a more organised, efficient, and connected health centre environment.
