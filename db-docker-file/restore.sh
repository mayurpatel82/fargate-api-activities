echo "Setting Environment variables."
export ACCEPT_EULA=Y
export SA_PASSWORD=Simple_Password
echo "Environment variables set."

echo "Starting SqlServr"
/opt/mssql/bin/sqlservr &
sleep 60 | echo "Waiting for 60s to start Sql Server"

echo "Setting RAM to 2GB usage."
/opt/mssql/bin/mssql-conf set memory.memorylimitmb 2048

echo "Restarting to apply the changes."
systemctl restart mssql-server.service

echo "Restoring DB."
/opt/mssql-tools/bin/sqlcmd -U sa -P $SA_PASSWORD -i $1
echo "DB restored."
