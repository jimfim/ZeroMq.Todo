import { Construct } from 'constructs';
import { App, Chart } from 'cdk8s';
import { WebService } from './lib/web-service';
export class MyChart extends Chart {
  constructor(scope: Construct, name: string) {
    super(scope, name);

    
    new WebService(this, 'query-handler', { 
      name: "query-handler",
      image: 'jimfim/query-handler:latest'
    });
    
    new WebService(this, 'query-proxy', { 
      name: "query-proxy",
      image: 'jimfim/query-proxy:latest'
    });

    new WebService()
    new WebService(this, 'gateway', { 
      name: "gateway",
      image: 'jimfim/gateway:latest'
    });
  }
}

const app = new App();
new MyChart(app, 'todo');
app.synth();
