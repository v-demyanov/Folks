import {
  HubConnection,
  HubConnectionBuilder,
  IHttpConnectionOptions,
} from '@microsoft/signalr';

const createHubConnection = (
  url: string,
  httpConnectionOptions: IHttpConnectionOptions,
  automaticReconnect = true
): HubConnection => {
  let hubConnectionBuilder = new HubConnectionBuilder().withUrl(
    url,
    httpConnectionOptions
  );

  if (automaticReconnect) {
    hubConnectionBuilder = hubConnectionBuilder.withAutomaticReconnect();
  }

  if (httpConnectionOptions.logger) {
    hubConnectionBuilder = hubConnectionBuilder.configureLogging(
      httpConnectionOptions.logger
    );
  }

  return hubConnectionBuilder.build();
};

export default createHubConnection;
