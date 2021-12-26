using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using alou149.Models;
using alou149.Data;
using alou149.Dto;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Net;

namespace alou149
{
    [Route("api")]
    [ApiController]
    public class theController : Controller
    {
        private readonly IProductAPIRepo _productRepository;
        private readonly IUsersAPIRepo _repository;
        private readonly IOrdersAPIRepo _orderRepository;
        public theController(IUsersAPIRepo repository, IProductAPIRepo productRepository, IOrdersAPIRepo orderRepository)
        {
            _repository = repository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
           
        }

        //ENDPOINT: 1
        //Users
        [HttpPost("Register")]
        public ActionResult AddNewUser(UserInputDto userInput)
        {            
            Users u = _repository.GetUserByName(userInput.UserName);
            IEnumerable<Users> uList = _repository.GetAllUsers();

            if (String.IsNullOrWhiteSpace(userInput.UserName))
            {
                return Ok("Invalid Username");
            }
            
            else if (uList.Contains(u))
            {
                return Ok("Username not available.");
            }
            else
            {
                Users theUser = new Users { UserName = userInput.UserName, Password = userInput.Password, Address = userInput.Address };
                Users addedComment = _repository.AddUser(theUser);              
                return Ok("User successfully registered.");
            }            
        }

        //ENDPOINT: 2
        //GetVersionA
        [Authorize(AuthenticationSchemes = "MyAuthentication")]
        [Authorize(Policy = "UserOnly")]
        [HttpGet("GetVersionA")]
        public ActionResult<UserOutputDto> ViewVersion()
        {
            ClaimsIdentity ci = HttpContext.User.Identities.FirstOrDefault();
            Claim c = ci.FindFirst("userName");
            string name = c.Value;
            Users u = _repository.GetUserByName(name);
            UserOutputDto cOut = new UserOutputDto { UserName = u.UserName, Address = u.Address, Password = u.Password };
            return Ok("v1");
        }

        //ENDPOINT: 3
        // PurchaseItem
        [Authorize(AuthenticationSchemes = "MyAuthentication")]
        [Authorize(Policy = "UserOnly")]
        [HttpPost("PurchaseItem")]
        public ActionResult<OrdersOutputDto> AddOrder(OrdersInputDto ordersInput)
        {
            ClaimsIdentity ci = HttpContext.User.Identities.FirstOrDefault();
            Claim c = ci.FindFirst("userName");
            string name = c.Value;
            Users u = _repository.GetUserByName(name);

            Orders theUser = new Orders { UserName = u.UserName, ProductID = ordersInput.ProductID, Quantity = ordersInput.Quantity };
            Orders addedOrder = _orderRepository.AddOrder(theUser);

            OrdersOutputDto cOut = new OrdersOutputDto { Id = addedOrder.Id, UserName = u.UserName, ProductID = ordersInput.ProductID, Quantity = ordersInput.Quantity };
            // return Ok(cOut);
            return CreatedAtAction(nameof(AddOrder), new { id = cOut.Id }, cOut);
        }

        //ENDPOINT: 4
        // PurchaseItem/{ID}
        [Authorize(AuthenticationSchemes = "MyAuthentication")]
        [Authorize(Policy = "UserOnly")]
        [HttpGet("PurchaseSingleItem/{item}")]
        public ActionResult<OrdersOutputDto> PurchaseItem(int item)
        {
            ClaimsIdentity ci = HttpContext.User.Identities.FirstOrDefault();
            Claim c = ci.FindFirst("userName");
            string name = c.Value;
            Users u = _repository.GetUserByName(name);
            Orders o = _orderRepository.GetOrderByOrderID(item);
            
            Orders theUser = new Orders { UserName = u.UserName, ProductID = item, Quantity =  1};
            Orders addedOrder = _orderRepository.AddOrder(theUser);
            OrdersOutputDto cOut = new OrdersOutputDto { Id = addedOrder.Id, UserName = u.UserName, ProductID = item, Quantity = 1 };
            //return Ok(cOut);
            return CreatedAtAction(nameof(PurchaseItem), new { id = cOut.Id }, cOut);
        }

    }
}
