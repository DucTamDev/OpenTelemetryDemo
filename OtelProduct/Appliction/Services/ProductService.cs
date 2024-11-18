using Appliction.Configurations.Interfaces.Repositories;
using Appliction.Configurations.Interfaces.Services;
using Appliction.Dtos;
using AutoMapper;
using Domain.Entities;
using OpenTelemetry.Trace;
using System.Diagnostics;
using System.Reflection;

namespace Appliction.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        readonly string serviceName = Assembly.GetExecutingAssembly().GetName().Name!;
        readonly string serviceVersion = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion
                                   ?? Assembly.GetExecutingAssembly().GetName()?.Version?.ToString() ?? "1.0.0";

        private readonly ActivitySource activitySource;
        private readonly TracerProvider _tracerProvider;


        public ProductService(IMapper mapper, IProductRepository productRepository, TracerProvider tracerProvider)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            activitySource = new ActivitySource("Alo");
            _tracerProvider = tracerProvider;
            //tracerProviderBuilder.AddSource(serviceName);
        }

        public IList<ProductDto> GetAllProducts()
        {
            //var listenerA = new ActivityListener()
            //{
            //    ShouldListenTo = _ => true,
            //    SampleUsingParentId = (ref ActivityCreationOptions<string> options) => ActivitySamplingResult.AllData,
            //    Sample = (ref ActivityCreationOptions<ActivityContext> options) => ActivitySamplingResult.AllData,
            //};


            //ActivitySource.AddActivityListener(listenerA);
            Tracer tracer = _tracerProvider.GetTracer(serviceName);

            var proxyIp = Activity.Current?.GetBaggageItem("proxy.ip");
            var userId = Activity.Current?.GetBaggageItem("user.userId");

            using (Activity? activity = activitySource.StartActivity("Application.ProductService"))
            {
                activity?.SetTag("method.name", "GetAllProducts");
                activity?.SetTag("user.userId", userId);
                activity?.SetTag("parent.proxyIp", proxyIp);

                var product = _productRepository.GetAll().ToList();

                var result = _mapper.Map<IList<ProductDto>>(product);
                return result;
            }

            //using (var span = tracer.StartSpan("Application.ProductService"))
            //{
            //    span.SetAttribute("function.name", "GetAllProduct");
            //    span.AddEvent("Get data from mssql server");

            //    var product = _productRepository.GetAll().ToList();

            //    var result = _mapper.Map<IList<ProductDto>>(product);
            //    return result;
            //}
        }

        public async Task<ProductDto> GetProductById(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<bool> CreateProduct(ProductDto productDto)
        {
            var product = _mapper.Map<ProductDto, Product>(productDto);

            _productRepository.Add(product);

            return await _productRepository.SaveChangeAsync() > 0;
        }
    }
}
