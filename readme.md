Prereq - fyr opp lenses box i docker

´´´´
docker run -e ADV_HOST=127.0.0.1 -e EULA="https://licenses.lenses.io/d/?id=933e42bc-9f60-11ef-9df4-42010af01003" --rm -p 3030:3030 -p 9092:9092 -p 8081:8081 lensesio/box:latest
´´´´


for å kjøre produceren:

```
cd producer
dotnet run C:\repos\kafka-dotnet-getting-started\getting-started.properties
dotnet run /home/jorn/repos/personligejorn/kafka-dotnet-getting-started/getting-started.properties
```