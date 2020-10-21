import { Construct, Node } from 'constructs';
import { Deployment, Service, IntOrString, HorizontalPodAutoscaler } from '../imports/k8s';
import { Names } from 'cdk8s';

export interface WebServiceOptions {

    /**
   * The name to use for this service.
   */
  readonly name: string;
  
  /**
   * The Docker image to use for this service.
   */
  readonly image: string;

  /**
   * Number of replicas.
   *
   * @default 2
   */
  readonly replicas?: number;

  /**
   * External port.
   *
   * @default 5000
   */
  readonly port?: number;

  /**
   * Internal port.
   *
   * @default 5000
   */
  readonly containerPort?: number;
}

export class WebService extends Construct {
  constructor(scope: Construct, ns: string, options: WebServiceOptions) {
    super(scope, ns);

    const port = options.port || 5000;
    const containerPort = options.containerPort || 5000;
    const label = { app: Names.toDnsLabel(Node.of(this).path) };
    const replicas = options.replicas ?? 2;

    new Service(this, 'service', {
      metadata:{
        name: options.name
      },
      spec: {
        type: 'ClusterIP',
        ports: [ { port, targetPort: IntOrString.fromNumber(containerPort) } ],
        selector: label,
      }
    });

    new HorizontalPodAutoscaler(this,'hpa', {
      spec: {
        scaleTargetRef:{ 
          apiVersion: 'v1',
          kind: 'Deployment',
          name: 'myAutoSCaler' 
        },
        maxReplicas: 10,
        minReplicas: 2,
        targetCPUUtilizationPercentage: 80
      }
    })

    new Deployment(this, 'deployment', {
      spec: {
        replicas,
        selector: {
          matchLabels: label
        },
        template: {
          metadata: { labels: label },
          spec: {
            containers: [
              {
                name: options.name,
                image: options.image,
                ports: [ { containerPort } ],
                env: [
                  {  
                    name: 'ENVIRONMENT',
                    value: 'Release'
                  }
                ]
              },
            ]
          }
        }
      }
    });
  }
}