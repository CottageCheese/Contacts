#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Staff/Contacts.csproj", "Staff/"]
COPY ["StaffLibrary/ContactsLibrary.csproj", "StaffLibrary/"]
COPY ["CoreBusinessLibrary/CoreBusinessLibrary.csproj", "CoreBusinessLibrary/"]
COPY ["StaffData/ContactsData.csproj", "StaffData/"]
RUN dotnet restore "Staff/Contacts.csproj"
COPY . .
WORKDIR "/src/Staff"
RUN dotnet build "Contacts.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Contacts.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Contacts.dll"]